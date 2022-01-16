export interface GetOrgizationTreeModel {
    id:       number;
    parentId: number;
    name:     string;
    code:     string;
    remark:   string;
    status:   number;
    children: getOrgizationTreeModel[];
}
