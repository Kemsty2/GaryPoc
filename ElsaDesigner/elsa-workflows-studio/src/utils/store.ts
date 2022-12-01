import { createStore } from "@stencil/store";
import Keycloak, { KeycloakProfile } from "keycloak-js";

const { state, onChange } = createStore({
  activityDescriptors: [],
  workflowStorageDescriptors: [],
  monacoLibPath: '',
  useX6Graphs: false,  
  userProfile: {} as KeycloakProfile,
  keycloak: {} as Keycloak
});

export default state;
