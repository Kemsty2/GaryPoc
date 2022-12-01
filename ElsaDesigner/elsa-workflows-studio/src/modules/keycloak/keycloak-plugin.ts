import Keycloak, { KeycloakConfig, KeycloakInitOptions } from 'keycloak-js';
import { Service } from 'axios-middleware';
import { eventBus, ElsaPlugin } from '../../services';
import { EventTypes } from '../../models';
import state from '../../utils/store';

export class KeycloakPlugin implements ElsaPlugin {
  private readonly options: KeycloakConfig;
  private keycloak: Keycloak;

  constructor(options: KeycloakConfig) {
    this.options = options;
    eventBus.on(EventTypes.Root.Initializing, this.initialize);
    eventBus.on(EventTypes.HttpClientCreated, this.configureAuthMiddleware);
  }

  private initialize = async () => {
    const options = this.options;
    const { url } = options;

    if (!url || url.trim().length == 0) return;

    this.keycloak = new Keycloak(options);
    const isAuthenticated = this.keycloak.authenticated;

    // Nothing to do if authenticated.
    if (isAuthenticated) return;

    // Redirect to Auth0 for the user to authenticate themselves.
    const origin = window.location.origin;

    const redirectOptions: KeycloakInitOptions = {
      redirectUri: origin,
      onLoad: 'login-required',
      checkLoginIframe: false,
    };

    const authenticated = await this.keycloak.init(redirectOptions);
    if (authenticated) {
      const profile = await this.keycloak.loadUserProfile();
      state.userProfile = profile;
      state.keycloak = this.keycloak;      
    }
  };

  private configureAuthMiddleware = async (e: any) => {
    const service: Service = e.service;
    const keycloak = this.keycloak;

    service.register({
      async onRequest(request) {
        // Get a (cached) access token.
        const token = keycloak.token;

        if (!!token) request.headers = { ...request.headers, Authorization: `Bearer ${token}` };
        return request;
      },
    });
  };
}
