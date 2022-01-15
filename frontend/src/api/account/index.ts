import { defHttp } from '/@/utils/http/axios';
import { LoginParams, GetCurrentUserinfo } from './model/accountModel';

import { ErrorMessageMode } from '/#/axios';

enum Api {
  Login = '/account/login',
  GetUserInfo = '/account/currentuserinfo',
}

export function loginApi(params: LoginParams, mode: ErrorMessageMode = 'modal') {
  return defHttp.post<string>(
    {
      url: Api.Login,
      params,
    },
    {
      errorMessageMode: mode,
    },
  );
}

export function getUserInfo() {
  return defHttp.get<GetCurrentUserinfo>({ url: Api.GetUserInfo }, { errorMessageMode: 'none' });
}

