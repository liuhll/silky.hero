import { defHttp } from '/@/utils/http/axios';

import { GetEditionFeatureModel, GetEditionModel } from './model/editionModel';
import { BasicFetchResult } from '../model/baseModel';

enum Api {
  GetEditionPage = '/edition/page',
  Edition = '/edition',
}

export const getEditionPageList = (requestParams: any) => {
  return defHttp.get<BasicFetchResult<GetEditionModel>>({
    url: Api.GetEditionPage,
    params: requestParams,
  });
};

export const createEdition = (requestParams: any) => {
  return defHttp.post({ url: Api.Edition, params: requestParams });
};

export const updateEdition = (requestParams: any) => {
  return defHttp.put({ url: Api.Edition, params: requestParams });
};

export const deleteEdition = (id: number) => {
  return defHttp.delete({ url: `/edition/${id}` });
};

export const getEditionById = (id: number) => {
  return defHttp.get<GetEditionFeatureModel>({ url: `/edition/${id}` });
};

export const setEditionFeatures = (id: number, requestParams: any[]) => {
  return defHttp.put({ url: `/edition/${id}/features`, params: requestParams });
};
