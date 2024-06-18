export function isAdminUser(user?: any)
{
    return user && user.roles.includes("Administrator")
}