import { BasicColumn } from '/@/components/Table';
import { FormSchema } from '/@/components/Table';
import { statusOptions } from '/@/utils/status';

import {
  getAllocationOrganizationRoles,
  getOrganizationTree,
  getOrganizationPositions,
  checkOrganization,
} from '/@/api/organization';
import { Status } from '/@/utils/status';
import { treeMap } from '/@/utils/helper/treeHelper';
import { TreeItem } from '/@/components/Tree';
import { GetOrgizationTreeModel } from '/@/api/organization/model/organizationModel';
import { DescItem } from '../../../components/Description/src/typing';
import { commonTagRender } from '/@/utils/tagUtil';
import { formatToDate } from '/@/utils/dateUtil';
import { omit } from 'lodash-es';
import { OptionsItem } from '/@/utils/model';
import { h } from 'vue';
import { Rule } from '/@/components/Form';

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
    colProps: { span: 12 },
  },
  {
    field: 'sort',
    label: '排序',
    component: 'InputNumber',
    componentProps: {
      style: 'width:100%',
    },
    colProps: { span: 12 },
  },
  {
    field: 'status',
    label: '状态',
    component: 'RadioButtonGroup',
    defaultValue: 1,
    componentProps: {
      options: statusOptions,
    },
    colProps: { span: 12 },
  },
  {
    field: 'remark',
    label: '备注',
    component: 'InputTextArea',
    colProps: { span: 24 },
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

export const getOrganizationTreeList = async (isOnlyBelong: boolean): Promise<TreeItem[]> => {
  const organizationTreeList = await getOrganizationTree();
  return treeMap(organizationTreeList, {
    conversion: (node: GetOrgizationTreeModel) => {
      const orgIcon =
        node.status == Status.Valid ? 'ant-design:folder-outlined' : 'ant-design:folder-filled';
      let disabled = node.status == Status.Invalid;
      if (isOnlyBelong) {
        disabled = disabled || node.isBelong === false;
      }
      return {
        title: node.name,
        key: node.id,
        value: node.id,
        disabled: disabled,
        icon: orgIcon,
      };
    },
  });
};

export const organizationDetailSchemas: DescItem[] = [
  {
    field: 'name',
    label: '名称',
  },
  {
    field: 'sort',
    label: '排序',
  },
  {
    label: '状态',
    field: 'status',
    render: (value) => {
      if (value === Status.Valid) {
        return commonTagRender('blue', '启用');
      } else {
        return commonTagRender('red', '停用');
      }
    },
  },
  {
    label: '分配角色',
    field: 'organizationRoles',
    render: (value) => {
      const labels = [];
      for (const item of value) {
        let text: any = h('span', { style: 'margin:3px 5px;' }, item.realName);
        if (item.isPublic) {
          text = commonTagRender('blue', item.realName);
        }
        labels.push(text);
      }
      return labels;
    },
  },
  {
    label: '分配职位',
    field: 'organizationPositions',
    render: (value) => {
      const labels = [];
      for (const item of value) {
        let text: any = h('span', { style: 'margin:3px 5px;' }, item.name);
        if (item.isPublic) {
          text = commonTagRender('blue', item.name);
        }
        labels.push(text);
      }
      return labels;
    },
  },
  {
    field: 'remark',
    label: '备注',
    span: 2,
  },
  {
    label: '创建时间',
    field: 'createdTime',
    render: (value) => {
      if (value) {
        return formatToDate(value, 'YYYY-MM-DD HH:MM:ss');
      }
      return null;
    },
  },
  {
    label: '最后更新时间',
    field: 'updatedTime',
    render: (value) => {
      if (value) {
        return formatToDate(value, 'YYYY-MM-DD HH:MM:ss');
      }
      return null;
    },
  },
];

export const getOrganizationRolesOptions = async () => {
  const roleList = await getAllocationOrganizationRoles();
  const roleOptions = roleList.reduce((prev, next: Recordable) => {
    if (next) {
      prev.push({
        ...omit(next),
        label: next['realName'],
        value: next['id'],
        disabled: next['status'] === Status.Invalid || next['isPublic'] === true,
      });
    }
    return prev;
  }, [] as OptionsItem[]);
  return roleOptions;
};

export const getOrganizationPositionsOptions = async () => {
  const positionList = await  getOrganizationPositions();
  const positionOptions = positionList.reduce((prev, next: Recordable) => {
    if (next) {
      prev.push({
        ...omit(next),
        label: next['name'],
        value: next['id'],
        disabled: next['status'] === Status.Invalid || next['isPublic'] === true,
      });
    }
    return prev;
  }, [] as OptionsItem[]);
  return positionOptions;
};

export const organizationRoleSchemas: FormSchema[] = [
  {
    field: 'roleIds',
    label: '角色',
    component: 'Select',
    slot: 'roleNamesSlot',
  },
];

export const organizationPositionSchemas: FormSchema[] = [
  {
    field: 'positionIds',
    label: '职位',
    component: 'Select',
    slot: 'positionNamesSlot',
  },
];

const checkOrganizationRule = async (
  value: string,
  id: Nullable<number>,
  parentId: Nullable<number>,
) => {
  if (value) {
    const exist = await checkOrganization({
      id: id,
      parentId: parentId,
      name: value,
    });
    if (exist) {
      return Promise.reject(`已经存在${value}的组织机构`);
    }
  }
  return Promise.resolve();
};

export const getNameRules = (id: Nullable<number>, parentId: Nullable<number>): Rule[] => {
  return [
    {
      required: true,
      message: '组织机构名称不允许为空',
    },
    {
      max: 50,
      message: '组织机构名称长度不允许超过50个字符',
      validateTrigger: ['change', 'blur'],
    },
    {
      type: 'string',
      validateTrigger: ['change', 'blur'],
      validator: (rules, value) => {
        return checkOrganizationRule(value, id, parentId);
      },
    },
  ];
};
