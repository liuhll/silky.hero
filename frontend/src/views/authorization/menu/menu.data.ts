import { BasicColumn } from '/@/components/Table';
import { FormSchema } from '/@/components/Table';
import { statusOptions } from '/@/utils/status';
import { Status } from '/@/utils/status';
import { Tag } from 'ant-design-vue';
import { Icon } from '/@/components/Icon';
import { h } from 'vue';
import { treeMap } from '/@/utils/helper/treeHelper';
import { TreeItem } from '/@/components/Tree';
import { checkMenu, getMenuTree } from '/@/api/menu';
import { GetMenuTreeModel } from '/@/api/menu/model/menuModel';
import { DescItem } from '/@/components/Description';
import { commonTagRender } from '/@/utils/tagUtil';
import { cp } from 'fs';
import { formatToDate } from '/@/utils/dateUtil';
import { Rule } from '/@/components/Form';

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
      let text = '';
      let color = '';
      if (isDir(record.type)) {
        text = '目录';
        color = 'green';
        return commonTagRender(color, text);
      }
      if (isMenu(record.type)) {
        text = '菜单';
        color = 'blue';
        return commonTagRender(color, text);
      }
      if (isButton(record.type)) {
        text = '按钮';
        color = 'purple';
      }
      return commonTagRender(color, text);
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
    title: '路由地址',
    dataIndex: 'routePath',
    width: 160,
    align: 'left',
  },
  {
    title: '排序',
    dataIndex: 'sort',
    width: 50,
    align: 'left',
  },
  {
    title: '状态',
    dataIndex: 'status',
    width: 50,
    customRender: ({ record }) => {
      const enable = record.status === Status.Valid;
      const color = enable ? 'green' : 'red';
      const text = enable ? '有效' : '无效';
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

export const getMenuDetailSchemas = (menuData): DescItem[] => [
  {
    field: 'name',
    label: '名称',
  },
  {
    field: 'type',
    label: '类型',
    render: (value) => {
      if (isDir(value)) {
        return commonTagRender('green', '目录');
      }
      if (isMenu(value)) {
        return commonTagRender('blue', '菜单');
      }
      if (isButton(value)) {
        return commonTagRender('purple', '按钮');
      }
      return null;
    },
  },
  {
    field: 'sort',
    label: '排序',
  },
  {
    field: 'icon',
    label: '图标',
    show: () => {
      return !isButton(menuData.type);
    },
  },

  {
    field: 'routePath',
    label: '路由地址',
    show: () => !isButton(menuData.type),
  },
  {
    field: 'component',
    label: '组件',
    show: () => !isButton(menuData.type),
  },
  {
    field: 'permissionCode',
    label: '权限标识',
    show: () => !isDir(menuData.type),
  },
  {
    field: 'status',
    label: '状态',
    render: (value) => {
      if (value === null) {
        return null;
      }
      return commonTagRender(value ? 'blue' : 'red', value ? '有效' : '无效');
    },
  },
  {
    field: 'externalLink',
    label: '是否外链',
    show: () => isMenu(menuData.type),
    render: (value) => {
      if (value === null) {
        return null;
      }
      return commonTagRender(value === true ? 'red' : 'blue', value ? '是' : '否');
    },
  },
  {
    field: 'externalLinkType',
    label: '外链类型',
    show: () => isMenu(menuData.type) && menuData.externalLink === true,
    render: (value) => {
      if (value == null) {
        return null;
      }
      return commonTagRender(value === 0 ? 'blue' : 'green', value ? '内嵌' : '外部');
    },
  },
  {
    field: 'keepAlive',
    label: '是否缓存',
    show: () => isMenu(menuData.type),
    render: (value) => {
      if (value === null) {
        return null;
      }
      return commonTagRender(value ? 'red' : 'blue', value ? '是' : '否');
    },
  },
  {
    field: 'display',
    label: '是否显示',
    show: () => !isButton(menuData.type),
    render: (value) => {
      if (value === null) {
        return null;
      }
      return commonTagRender(value ? 'blue' : 'red', value ? '是' : '否');
    },
  },
  {
    field: 'hideChildrenInMenu',
    label: '隐藏子菜单',
    show: () => isDir(menuData.type),
    render: (value) => {
      if (value === null) {
        return null;
      }
      return commonTagRender(value ? 'red' : 'blue', value ? '是' : '否');
    },
  },
  {
    field: 'currentActiveMenu',
    label: '当前活动菜单',
    show: () => isMenu(menuData.type) && menuData.display === false,
  },
  {
    field: 'hideBreadcrumb',
    label: '隐藏面包屑',
    show: () => !isButton(menuData.type),
    render: (value) => {
      if (value === null) {
        return null;
      }
      return commonTagRender(value ? 'red' : 'blue', value ? '是' : '否');
    },
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

const checkMenuRule = async (value: string, id: Nullable<number>, parentId: Nullable<number>) => {
  if (value) {
    const exist = await checkMenu({
      id: id,
      parentId: parentId,
      name: value,
    });
    if (exist) {
      return Promise.reject(`已经存在${value}的菜单`);
    }
  }
  return Promise.resolve();
};

export const getNameRules = (id: Nullable<number>, parentId: Nullable<number>): Rule[] => {
  return [
    {
      required: true,
      message: '名称不允许为空',
    },
    {
      max: 50,
      message: '名称长度不允许超过50个字符',
      validateTrigger: ['change', 'blur'],
    },
    {
      type: 'string',
      validateTrigger: ['change', 'blur'],
      validator: (rules, value) => {
        return checkMenuRule(value, id, parentId);
      },
    },
  ];
};
