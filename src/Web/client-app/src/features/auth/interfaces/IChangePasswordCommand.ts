export interface IChangePasswordCommand {
  values: { currentPassword: string; newPassword: string };
  actions: any;
  setApiError: any;
}
