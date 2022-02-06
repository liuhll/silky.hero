export enum DataRange {
  /// <summary>
  /// 所有部门数据
  /// </summary>
  Whole = 0,

  /// <summary>
  /// 本部门数据
  /// </summary>
  SelfOrganization = 1,

  /// <summary>
  /// 本部门及子部门数据
  /// </summary>
  SelfAndChildrenOrganization = 2,

  /// <summary>
  /// 自定义数据权限
  /// </summary>
  CustomOrganization = 3,
}

export const DataRangeOptions = [
  {
    label: '所有部门数据',
    value: DataRange.Whole,
  },
  {
    label: '本部门数据',
    value: DataRange.SelfOrganization,
  },
  {
    label: '本部门及子部门数据',
    value: DataRange.SelfAndChildrenOrganization,
  },
  {
    label: '自定义数据权限',
    value: DataRange.CustomOrganization,
  },
];
