export interface GetMenuPageModel {
  name: string;
  permissionCode: string;
  parentId: number;
  icon: string;
  sort: number;
  routePath: string;
  component: string;
  status: number;
  externalLink: boolean;
  display: boolean;
  cache: boolean;
  type: number;
  id: number;
  children: GetMenuPageModel[];
}

export interface GetMenuTreeModel {
  name: string;
  permissionCode: string;
  parentId: number;
  icon: string;
  sort: number;
  routePath: string;
  component: string;
  status: number;
  externalLink: boolean;
  display: boolean;
  cache: boolean;
  type: number;
  id: number;
  children: GetMenuTreeModel[];
}
