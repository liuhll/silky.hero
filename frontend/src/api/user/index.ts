import { defHttp } from '/@/utils/http/axios';
import { BasicFetchResult } from '/@/api/model/baseModel';
import { GetUserPageModel } from './model/userModel';


enum Api {
  GetUserPageList = '/user/page',
}

export const getUserPageList = (requestParams) => {
  return defHttp.get<BasicFetchResult<GetUserPageModel>>({
    url: Api.GetUserPageList,
    params: requestParams
  })
}


