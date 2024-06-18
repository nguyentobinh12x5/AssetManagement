export interface ILoginCommand {
    email: string;
    password: string;
    twoFactorCode?: string;
    twoFactorRecoveryCode?: string;
    useCookies?: boolean;
    useSessionCookies?: boolean;
}