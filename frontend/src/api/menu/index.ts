import { defHttp } from '/@/utils/http/axios';
import { BasicFetchResult } from '/@/api/model/baseModel';
import { GetMenuPageModel, GetMenuTreeModel } from './model/menuModel';

enum Api {
  GetMenuPageList = '/menu/page',
  GetMenuTree = '/menu/tree',
  Menu = '/menu',
}

export const getMenuPageList = (requestParams) => {
  return defHttp.get<BasicFetchResult<GetMenuPageModel>>({
    url: Api.GetMenuPageList,
    params: requestParams,
  });
};

export const getMenuTree = (requestParams: any) => {
  return defHttp.get<GetMenuTreeModel[]>({
    url: Api.GetMenuTree,
    params: requestParams,
  });
};

export const createMenu = (requestParams) => {
  return defHttp.post({
    url: Api.Menu,
    params: requestParams,
  });
};

export const updateMenu = (requestParams) => {
  return defHttp.put({
    url: Api.Menu,
    params: requestParams,
  });
};

export const deleteMenu = (id: number) => {
  return defHttp.delete({
    url: `/menu/${id}`,
  });
};

// export const setMenuStatus = (id: number, status: Status) => {
//   return defHttp.put({
//     url: `/menu/status/${id}/${status}`,
//   });
// };
