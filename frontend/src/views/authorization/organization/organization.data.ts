import { BasicColumn } from '/@/components/Table';
import { FormSchema } from '/@/components/Table';
import { statusOptions } from '/@/utils/status';

import { getOrganizationTree } from '/@/api/organization';
import { Status } from '/@/utils/status';
import { treeMap } from '/@/utils/helper/treeHelper';
import { TreeItem } from '/@/components/Tree';
import { GetOrgizationTreeModel } from '/@/api/organization/model/organizationModel';

export const userColumns: BasicColumn[] = [
  {
    title: '用户名',
    dataIndex: 'userName',
    width: 100,
  },
  {
    title: '真实姓名',
    dataIndex: 'realName',
    width: 100,
  },
  {
    title: '邮箱',
    dataIndex: 'email',
    width: 120,
  },
  {
    title: '手机',
    dataIndex: 'telPhone',
    width: 100,
  },
  {
    title: '工号',
    dataIndex: 'jobNumber',
    width: 100,
  },
  {
    title: '岗位',
    dataIndex: 'positionName',
    width: 100,
  },
];

export const organizationUserColumns: BasicColumn[] = [
  {
    title: '用户名',
    dataIndex: 'userName',
    width: 120,
  },
  {
    title: '真实姓名',
    dataIndex: 'realName',
    width: 120,
  },
  {
    title: '邮箱',
    dataIndex: 'email',
    width: 120,
  },
  {
    title: '岗位',
    dataIndex: 'positionId',
    slots: { customRender: 'position' },
    width: 200,
  },
];

export const organizationFormSchema: FormSchema[] = [
  {
    field: 'name',
    label: '名称',
    component: 'Input',
    helpMessage: ['请输入组织机构名称'],
    rules: [
      {
        required: true,
        message: '请输入用户名',
      },
    ],
  },
  {
    field: 'sort',
    label: '排序',
    component: 'InputNumber',
    helpMessage: ['请输入排序'],
  },
  {
    field: 'status',
    label: '状态',
    component: 'RadioButtonGroup',
    defaultValue: 1,
    componentProps: {
      options: statusOptions,
    },
    helpMessage: ['请选择状态'],
  },
  {
    field: 'remark',
    label: '备注',
    component: 'Input',
    helpMessage: ['请输入备注'],
  },
];

export const searchFormSchema: FormSchema[] = [
  {
    field: 'userName',
    label: '用户名',
    component: 'Input',
    colProps: { span: 8 },
  },
  {
    field: 'realName',
    label: '真实姓名',
    component: 'Input',
    colProps: { span: 8 },
  },
];

export const getOrganizationTreeList = async (): Promise<TreeItem[]> => {
  const organizationTreeList = await getOrganizationTree();
  return treeMap(organizationTreeList, {
    conversion: (node: GetOrgizationTreeModel) => {
      return {
        title: node.name,
        key: node.id,
        value: node.id,
        disabled: node.status == Status.Invalid,
      };
    },
  });
};
