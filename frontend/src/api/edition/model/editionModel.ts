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
