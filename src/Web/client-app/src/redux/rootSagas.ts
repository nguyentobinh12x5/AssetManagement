import todoItemSagas from '../features/todo-item/sagas';
import userSagas from '../features/manager-user/sagas';
import authSagas from '../features/auth/sagas';
import changePasswordSagas from '../features/auth/changepassword/sagas';
import assetSagas from '../features/asset/sagas';

const rootSagas = [
  todoItemSagas,
  authSagas,
  changePasswordSagas,
  userSagas,
  assetSagas,
];
export default rootSagas;
