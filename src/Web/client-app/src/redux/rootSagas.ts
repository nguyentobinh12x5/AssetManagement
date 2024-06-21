import todoItemSagas from '../features/todo-item/sagas';
import authSagas from '../features/auth/sagas';
import changePasswordSagas from '../features/auth/changepassword/sagas';
import userSagas from '../features/manage-user/sagas';

export default [todoItemSagas, authSagas, changePasswordSagas, userSagas];
