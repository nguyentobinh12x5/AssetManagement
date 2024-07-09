const ENDPOINTS = {
  AUTHORIZE: 'api/Auth/login',
  USER_INFO: 'api/Auth/manage/info',
  CHANGE_PWD_FIRST_TIME: 'api/Auth/change-password-first-time',
  LOGOUT: 'api/Auth/logout',

  TODOITEM: 'api/TodoItems',
  CHANGE_PASSWORD: '/api/Auth/change-password',
  USER: 'api/Users',
  DELETE_USER: '/api/users',

  ASSETS: '/api/Assets',
    RETURNINGS: '/api/ReturningRequests',
  ASSIGNMENTS: '/api/Assignments',
  MY_ASSIGNMENTS: '/api/Assignments/me',

  UPDATE_STATE_ASSIGNMENT: '/api/Assignments',
  CREATE_RETURNING_REQUEST: '/api/ReturningRequests/Create',
};

export default ENDPOINTS;
