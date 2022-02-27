import { defHttp } from '/@/utils/http/axios';

import { GetEditionFeatureModel, GetEditionListModel, GetEditionModel } from './model/editionModel';
import { BasicFetchResult } from '../model/baseModel';

enum Api {
  GetEditionPage = '/edition/page',
  Edition = '/edition',
  GetEditionList = '/edition/list',
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

export const getEditionList = () => {
  return defHttp.get<GetEditionListModel[]>({ url: Api.GetEditionList });
};

export const checkEdition = (requestParams) => {
  return defHttp.post({ url: `/edition/check`, params: requestParams });
};
