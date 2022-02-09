import { defHttp } from '/@/utils/http/axios';
import { LoginParams, GetCurrentUserinfo } from './model/accountModel';
import { getMenuListResultModel } from './model/menuModel';
import { ErrorMessageMode } from '/#/axios';

enum Api {
  Login = '/account/login',
  GetUserInfo = '/account/currentuserinfo',
  GetUserMenus = '/account/menus',
  GetPermCode = 'account/permissioncodes',
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

export function getMenuList() {
  return defHttp.get<getMenuListResultModel>({ url: Api.GetUserMenus });
}

export function getPermCode() {
  return defHttp.get<string[]>({ url: Api.GetPermCode });
}
