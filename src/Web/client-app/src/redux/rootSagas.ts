import todoItemSagas from '../features/todo-item/sagas';
import userSagas from '../features/manager-user/sagas';
import authSagas from '../features/auth/sagas';
import changePasswordSagas from '../features/auth/changepassword/sagas';

export default [todoItemSagas, authSagas, userSagas, changePasswordSagas];
