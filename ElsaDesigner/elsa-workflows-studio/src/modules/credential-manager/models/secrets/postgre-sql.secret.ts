import { Secret } from "../secret.model";

export const PostgreSql: Secret = {
  category: "Sql",
  customAttributes: {},
  description: "Sql connection data",
  displayName: "PostgreSql secret",
  inputProperties: [
    {
      disableWorkflowProviderSelection: false,
      isBrowsable: true,
      isReadOnly: false,
      label: "Host",
      name: "Host",
      order: 0,
      supportedSyntaxes: ["JavaScript", "Liquid"],
      type: "System.String",
      uiHint: "single-line",
    },
    {
      disableWorkflowProviderSelection: false,
      isBrowsable: true,
      isReadOnly: false,
      label: "Port",
      name: "Port",
      order: 0,
      supportedSyntaxes: ["JavaScript", "Liquid"],
      type: "System.Int64",
      uiHint: "single-line",
    },
    {
      disableWorkflowProviderSelection: false,
      isBrowsable: true,
      isReadOnly: false,
      label: "Database",
      name: "Database",
      order: 0,
      supportedSyntaxes: ["JavaScript", "Liquid"],
      type: "System.String",
      uiHint: "single-line",
    },
    {
      disableWorkflowProviderSelection: false,
      isBrowsable: true,
      isReadOnly: false,
      label: "Username",
      name: "Username",
      order: 0,
      supportedSyntaxes: ["JavaScript", "Liquid"],
      type: "System.String",
      uiHint: "single-line",
    },
    {
      disableWorkflowProviderSelection: false,
      isBrowsable: true,
      isReadOnly: false,
      label: "Password",
      name: "Password",
      order: 0,
      supportedSyntaxes: ["JavaScript", "Liquid"],
      type: "System.String",
      uiHint: "single-line",
    }
  ],
  type: "PostgreSql"
}
