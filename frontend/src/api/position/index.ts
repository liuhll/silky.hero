import { defHttp } from '/@/utils/http/axios';
import { BasicFetchResult } from '/@/api/model/baseModel';

import { GetPositionModel, GetPositionPageModel } from './model/positionModel';

enum Api {
  GetPositionList = '/position/list',
  GetPositionPage = '/position/page',
  Position = '/position',
}

export const getPositionList = (query: any) => {
  return defHttp.get<GetPositionModel[]>({
    url: Api.GetPositionList,
    params: { name: query.name },
  });
};

export const getPositionPageList = (requestParams: any) => {
  return defHttp.get<BasicFetchResult<GetPositionPageModel>>({
    url: Api.GetPositionPage,
    params: requestParams,
  });
};

export const createPosition = (requestParams: any) => {
  return defHttp.post({ url: Api.Position, params: requestParams });
};

export const updatePosition = (requestParams: any) => {
  return defHttp.put({ url: Api.Position, params: requestParams });
};

export const deletePosition = (id: number) => {
  return defHttp.delete({ url: `/position/${id}` });
};

export const getPositionById = (id: number) => {
  return defHttp.get({ url: `/position/${id}` });
};
