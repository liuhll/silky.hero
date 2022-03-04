import { omit } from 'lodash-es';
import { statusOptions } from '/@/utils/status';
import { Status } from '/@/utils/status';
import { Tag } from 'ant-design-vue';
import { h } from 'vue';
import { BasicColumn } from '/@/components/Table';
import { FormSchema } from '/@/components/Table';
import { DescItem } from '../../../components/Description/src/typing';
import { commonTagRender } from '/@/utils/tagUtil';
import { formatToDate } from '/@/utils/dateUtil';
import { Rule } from '/@/components/Form';
import { checkTenant } from '/@/api/tenant';

export enum TenantType {
  Name = 0,
  RealName = 1,
}

export const columns: BasicColumn[] = [
  {
    title: '标识',
    dataIndex: 'name',
    width: 120,
  },
  {
    title: '名称',
    dataIndex: 'realName',
    width: 120,
  },
  {
    title: '状态',
    dataIndex: 'status',
    width: 80,
    customRender: ({ record }) => {
      const enable = record.status === Status.Valid;
      const color = enable ? 'green' : 'red';
      const text = enable ? '正常' : '冻结';
      return h(Tag, { color: color }, () => text);
    },
  },
  {
    title: '排序',
    dataIndex: 'sort',
    width: 50,
  },
  {
    title: '版本',
    dataIndex: 'editionName',
    width: 80,
  },
  {
    title: '备注',
    dataIndex: 'remark',
    width: 120,
    align: 'left',
  },
];

export const searchFormSchema: FormSchema[] = [
  {
    field: 'name',
    label: '标识',
    component: 'Input',
    colProps: { span: 6 },
  },
  {
    field: 'status',
    label: '状态',
    component: 'Select',
    componentProps: {
      options: statusOptions,
      style: 'width:100%',
    },
    colProps: { span: 6 },
  },
];

export const getTenantSchemas = (isUpdate: boolean): FormSchema[] => {
  let schemas: FormSchema[] = [
    {
      field: 'divider-tenantinfo',
      component: 'Divider',
      label: '租户信息',
      ifShow: !isUpdate,
    },
    {
      field: 'name',
      component: 'Input',
      label: '标识',
    },
    {
      field: 'realName',
      component: 'Input',
      label: '名称',
    },
    {
      field: 'sort',
      component: 'InputNumber',
      label: '排序',
      componentProps: {
        style: 'width: 100%',
      },
    },
    {
      field: 'editionId',
      label: '版本',
      component: 'Select',
    },
    {
      field: 'status',
      label: '状态',
      component: 'RadioButtonGroup',
      componentProps: {
        options: statusOptions,
      },
      defaultValue: Status.Valid,
    },
    {
      field: 'remark',
      component: 'InputTextArea',
      label: '备注',
    },
  ];
  const otherSchemas: FormSchema[] = [
    {
      field: 'divider-tenantuserinfo',
      component: 'Divider',
      label: '初始管理员用户信息',
    },
    {
      field: 'superUserName',
      component: 'Input',
      label: '用户名',
      helpMessage: '英文字符和数字组合,不允许超过50个字符',
      required: true,
      colProps: {
        span: 12,
      },
      rules: [
        {
          required: true,
          message: '用户名不允许为空',
        },
        {
          max: 50,
          message: '用户名长度不允许超过50个字符',
          validateTrigger: ['change', 'blur'],
        },
        {
          type: 'string',
          pattern: new RegExp('^\\w+$'),
          message: '用户名格式不正确',
          validateTrigger: ['change', 'blur'],
        },
      ],
    },
    {
      field: 'superRealName',
      component: 'Input',
      label: '真实姓名',
      colProps: {
        span: 12,
      },
      rules: [
        {
          required: true,
          message: '真实姓名不允许为空',
        },
        {
          max: 50,
          message: '用户真实姓名长度不允许超过50个字符',
          validateTrigger: ['change', 'blur'],
        },
      ],
    },
    {
      field: 'superPassword',
      component: 'InputPassword',
      label: '密码',
      colProps: {
        span: 12,
      },
      helpMessage: '至少8位，包含大小写、数字和特殊符号',
      rules: [
        {
          required: true,
          message: '密码不允许为空',
        },
        {
          type: 'string',
          pattern: new RegExp('^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$'),
          message: '密码格式不正确',
          validateTrigger: ['change', 'blur'],
        },
      ],
    },
    {
      field: 'superUserEmail',
      component: 'Input',
      label: '电子邮件',
      colProps: {
        span: 12,
      },
      rules: [
        {
          required: true,
          message: '电子邮件格式不允许为空',
        },
        {
          type: 'email',
          message: '电子邮件格式不正确',
          validateTrigger: ['change', 'blur'],
        },
      ],
    },
    {
      field: 'superUserMobilePhone',
      component: 'Input',
      label: '手机',
      colProps: {
        span: 12,
      },
      rules: [
        {
          required: true,
          message: '手机号码不允许为空',
        },
        {
          type: 'string',
          pattern: new RegExp('^((13[0-9])|(14[5|7])|(15([0-3]|[5-9]))|(18[0,5-9]))\\d{8}$'),
          message: '手机格式不正确',
          validateTrigger: ['change', 'blur'],
        },
      ],
    },
    {
      field: 'superUserJobNumber',
      component: 'Input',
      label: '工号',
      colProps: {
        span: 12,
      },
      rules: [
        {
          required: true,
          message: '工号不允许为空',
        },
        {
          type: 'string',
          pattern: new RegExp('^\\w+$'),
          message: '工号格式不正确',
          validateTrigger: ['change', 'blur'],
        },
      ],
    },
    {
      field: 'divider-tenantrole',
      component: 'Divider',
      label: '初始管理角色信息',
      ifShow: true,
    },
    {
      field: 'superRoleName',
      component: 'Input',
      label: '角色标识',
      helpMessage: '英文字符和数字组合,不允许超过50个字符',
      rules: [
        {
          required: true,
          message: '角色不允许为空',
        },
        {
          max: 50,
          message: '角色标识长度不允许超过50个字符',
          validateTrigger: ['change', 'blur'],
        },
        {
          type: 'string',
          pattern: new RegExp('^\\w+$'),
          message: '角色格式不正确',
          validateTrigger: ['change', 'blur'],
        },
      ],
      colProps: {
        span: 12,
      },
    },
    {
      field: 'superRoleRealName',
      component: 'Input',
      label: '角色名称',
      rules: [
        {
          required: true,
          message: '角色名称不允许为空',
        },
        {
          max: 50,
          message: '角色名称长度不允许超过50个字符',
          validateTrigger: ['change', 'blur'],
        },
      ],
      colProps: {
        span: 12,
      },
    },
  ];
  if (!isUpdate) {
    schemas = schemas.concat(otherSchemas);
  }
  return schemas;
};

export const tenantDetailSchemas: DescItem[] = [
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
        return commonTagRender('green', '正常');
      } else {
        return commonTagRender('red', '冻结');
      }
    },
  },
  {
    field: 'editionName',
    label: '版本',
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

const checkTenantRule = async (value: string, nameType: TenantType, id: Nullable<number>) => {
  if (value) {
    const exist = await checkTenant({
      id: id,
      nameType: nameType,
      name: value,
    });
    if (exist) {
      return Promise.reject(`已经存在${value}的租户`);
    }
  }
  return Promise.resolve();
};

export const getNameRules = (id: Nullable<number>): Rule[] => {
  return [
    {
      required: true,
      message: '租户标识不允许为空',
    },
    {
      max: 50,
      message: '租户标识长度不允许超过50个字符',
      validateTrigger: ['change', 'blur'],
    },
    {
      type: 'string',
      validateTrigger: ['change', 'blur'],
      validator: (rules, value) => {
        return checkTenantRule(value, TenantType.Name, id);
      },
    },
  ];
};

export const getRealNameRules = (id: Nullable<number>): Rule[] => {
  return [
    {
      required: true,
      message: '租户名称不允许为空',
    },
    {
      max: 50,
      message: '租户名称长度不允许超过50个字符',
      validateTrigger: ['change', 'blur'],
    },
    {
      type: 'string',
      validateTrigger: ['change', 'blur'],
      validator: (rules, value) => {
        return checkTenantRule(value, TenantType.RealName, id);
      },
    },
  ];
};
