import { DataNode } from 'ant-design-vue/lib/tree';

export type MenuItem = {
  group: String;
  checkNum: Number;
  checkAll: Boolean;
  indeterminate: Boolean;
  treeData: DataNode[] | undefined;
  checkedMenuIds: Number[];
};
