import { BasicColumn } from '/@/components/Table';
import { FormSchema } from '/@/components/Table';
import { statusOptions } from '/@/utils/status';
import { Status } from '/@/utils/status';
import { Tag } from 'ant-design-vue';
import { Icon } from '/@/components/Icon';
import { h } from 'vue';
import { treeMap } from '/@/utils/helper/treeHelper';
import { TreeItem } from '/@/components/Tree';
import { getMenuTree } from '/@/api/menu';
import { GetMenuTreeModel } from '/@/api/menu/model/menuModel';

const isDir = (type: number) => type === 0;
const isMenu = (type: number) => type === 1;
const isButton = (type: number) => type === 2;

export const columns: BasicColumn[] = [
  {
    title: '名称',
    dataIndex: 'name',
    width: 150,
    align: 'left',
  },
  {
    title: '类型',
    dataIndex: 'type',
    width: 50,
    customRender: ({ record }) => {
      if (isDir(record.type)) {
        const text = '目录';
        const color = 'green';
        return h(Tag, { color: color }, () => text);
      }
      if (isMenu(record.type)) {
        const text = '菜单';
        const color = 'blue';
        return h(Tag, { color: color }, () => text);
      }
      if (isButton(record.type)) {
        const text = '按钮';
        const color = 'purple';
        return h(Tag, { color: color }, () => text);
      }
      return '';
    },
  },
  {
    title: '图标',
    dataIndex: 'icon',
    width: 50,
    customRender: ({ record }) => {
      if (record.icon) {
        return h(Icon, { icon: record.icon });
      }
      return '';
    },
  },
  {
    title: '权限标识',
    dataIndex: 'permissionCode',
    width: 200,
    align: 'left',
  },
  {
    title: '组件',
    dataIndex: 'component',
    width: 250,
    align: 'left',
  },
  {
    title: '外链',
    dataIndex: 'externalLink',
    width: 50,
  },
  {
    title: '路由地址',
    dataIndex: 'routePath',
    width: 160,
    align: 'left',
  },
  {
    title: '状态',
    dataIndex: 'status',
    width: 50,
    customRender: ({ record }) => {
      const enable = record.status === Status.Valid;
      const color = enable ? 'green' : 'red';
      const text = enable ? '启用' : '停用';
      return h(Tag, { color: color }, () => text);
    },
  },
];

export const searchFormSchema: FormSchema[] = [
  {
    field: 'name',
    label: '菜单名称',
    component: 'Input',
    colProps: { span: 6 },
  },
];

export const menuSchemas: FormSchema[] = [
  {
    field: 'type',
    label: '菜单类型',
    component: 'RadioButtonGroup',
    defaultValue: 0,
    componentProps: {
      options: [
        { label: '目录', value: 0 },
        { label: '菜单', value: 1 },
        { label: '按钮', value: 2 },
      ],
    },
    required: true,
    colProps: { lg: 24, md: 24 },
  },
  {
    field: 'name',
    label: '名称',
    component: 'Input',
    required: true,
  },

  {
    field: 'parentId',
    label: '上级菜单',
    component: 'TreeSelect',
    componentProps: {
      fieldNames: {
        label: 'name',
        key: 'id',
        value: 'id',
      },
      getPopupContainer: () => document.body,
    },
  },

  {
    field: 'sort',
    label: '排序',
    component: 'InputNumber',
    required: true,
  },
  {
    field: 'icon',
    label: '图标',
    component: 'IconPicker',
    ifShow: ({ values }) => !isButton(values.type),
  },

  {
    field: 'routePath',
    label: '路由地址',
    component: 'Input',
    required: true,
    ifShow: ({ values }) => !isButton(values.type),
  },
  {
    field: 'component',
    label: '组件',
    component: 'Input',
    ifShow: ({ values }) => !isButton(values.type),
  },
  {
    field: 'permissionCode',
    label: '权限标识',
    component: 'Input',
    ifShow: ({ values }) => !isDir(values.type),
  },
  {
    field: 'status',
    label: '状态',
    component: 'RadioButtonGroup',
    defaultValue: Status.Valid,
    componentProps: {
      options: statusOptions,
    },
  },
  {
    field: 'externalLink',
    label: '是否外链',
    component: 'RadioButtonGroup',
    defaultValue: false,
    componentProps: {
      options: [
        { label: '否', value: false },
        { label: '是', value: true },
      ],
    },
    ifShow: ({ values }) => isMenu(values.type),
  },
  {
    field: 'externalLinkType',
    label: '外链类型',
    component: 'RadioButtonGroup',
    required: true,
    componentProps: {
      options: [
        { label: '内嵌', value: 0 },
        { label: '外部', value: 1 },
      ],
    },
    ifShow: ({ values }) => isMenu(values.type) && values.externalLink === true,
  },
  {
    field: 'keepAlive',
    label: '是否缓存',
    component: 'RadioButtonGroup',
    defaultValue: true,
    componentProps: {
      options: [
        { label: '否', value: false },
        { label: '是', value: true },
      ],
    },
    ifShow: ({ values }) => isMenu(values.type),
  },
  {
    field: 'display',
    label: '是否显示',
    component: 'RadioButtonGroup',
    defaultValue: true,
    componentProps: {
      options: [
        { label: '是', value: true },
        { label: '否', value: false },
      ],
    },
    ifShow: ({ values }) => !isButton(values.type),
  },
  {
    field: 'hideChildrenInMenu',
    label: '隐藏子菜单',
    component: 'RadioButtonGroup',
    defaultValue: false,
    componentProps: {
      options: [
        { label: '否', value: false },
        { label: '是', value: true },
      ],
    },
    ifShow: ({ values }) => isDir(values.type),
  },
  {
    field: 'currentActiveMenu',
    label: '当前活动菜单',
    component: 'Input',
    ifShow: ({ values }) => isMenu(values.type) && values.display == false,
  },
  {
    field: 'hideBreadcrumb',
    label: '是否隐藏面包屑',
    component: 'RadioButtonGroup',
    defaultValue: false,
    componentProps: {
      options: [
        { label: '否', value: false },
        { label: '是', value: true },
      ],
    },
    ifShow: ({ values }) => !isButton(values.type),
  },
];

export const getMenuTreeList = async (requestParams: any): Promise<TreeItem[]> => {
  const organizationTreeList = await getMenuTree(requestParams);
  return treeMap(organizationTreeList, {
    conversion: (node: GetMenuTreeModel) => {
      return {
        name: node.name,
        id: node.id,
      };
    },
  });
};

export const getMenuTreeList2 = async (requestParams: any): Promise<TreeItem[]> => {
  const organizationTreeList = await getMenuTree(requestParams);
  return treeMap(organizationTreeList, {
    conversion: (node: GetMenuTreeModel) => {
      return {
        title: node.name,
        key: node.id,
        value: node.id,
        disabled: node.status === Status.Invalid,
      };
    },
  });
};
