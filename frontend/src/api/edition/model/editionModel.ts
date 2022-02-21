export interface GetEditionFeatureModel {
  id: number;
  name: string;
  featureCatalogs: GetFeatureCatalogModel[];
}

export interface GetFeatureCatalogModel {
  name: string;
  features: GetFeatureModel[];
}

export interface GetFeatureModel {
  name: string;
  featureType: number;
  description: string;
  options: null;
  featureId: number;
  featureValue: number;
}

export interface GetEditionModel {
  id: number;
  name: string;
  price: null;
}
export interface GetEditionListModel {
  id: number;
  name: string;
}
