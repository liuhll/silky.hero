import { BasicColumn } from '/@/components/Table';
import { FormSchema } from '/@/components/Table';
import { sexOptions } from '/@/utils/sex';
import { formatToDate } from '/@/utils/dateUtil';
import { Sex } from '/@/utils/sex';
import { statusOptions, Status } from '/@/utils/status';
import { DescItem } from '/@/components/Description/index';
import { commonTagRender } from '/@/utils/tagUtil';
import { checkAccount } from '/@/api/user';
import { Rule } from '/@/components/Form';
import { OptionsItem } from '/@/utils/model';

enum AccountType {
  UserName = 0,
  MobilePhone = 1,
  Email = 2,
  JobNumber = 3,
}

export const columns: BasicColumn[] = [
  {
    title: '用户名',
    dataIndex: 'userName',
    width: 60,
  },
  {
    title: '真实姓名',
    dataIndex: 'realName',
    width: 60,
  },
  {
    title: '邮箱',
    dataIndex: 'email',
    width: 100,
  },
  {
    title: '手机',
    dataIndex: 'mobilePhone',
    width: 100,
  },
  {
    title: '工号',
    dataIndex: 'jobNumber',
    width: 60,
  },
  {
    title: '是否被锁定',
    dataIndex: 'isLockout',
    width: 60,
    align: 'left',
    slots: { customRender: 'isLockout' },
  },
  {
    title: '状态',
    dataIndex: 'status',
    align: 'left',
    width: 50,
    format: (value: Status) => {
      const colorValue = value === Status.Valid ? 'green' : 'red';
      const valText = value === Status.Valid ? '正常' : '冻结';
      return commonTagRender(colorValue, valText);
    },
  },
  {
    title: '昵称',
    dataIndex: 'surname',
    width: 80,
    defaultHidden: true,
  },
  {
    title: '生日',
    dataIndex: 'birthDay',
    format: (value) => {
      return formatToDate(value, 'YYYY-MM-DD');
    },
    width: 80,
    defaultHidden: true,
  },
  {
    title: '性别',
    dataIndex: 'sex',
    align: 'left',
    format: (value) => {
      if (`${value}` === `${Sex.Female}`) {
        return '女';
      }
      if (`${value}` === `${Sex.Male}`) {
        return '男';
      }

      return null;
    },
    width: 30,
    defaultHidden: true,
  },
  {
    title: '部门(职位)',
    dataIndex: 'userSubsidiaries',
    width: 150,
    align: 'left',
    slots: { customRender: 'userSubsidiaries' },
  },
  {
    title: '角色',
    dataIndex: 'roles',
    align: 'left',
    width: 150,
    slots: { customRender: 'roles' },
  },
];

export const searchFormSchema: FormSchema[] = [
  {
    field: 'userName',
    label: '用户名',
    component: 'Input',
    colProps: { span: 6 },
  },
  {
    field: 'realName',
    label: '真实姓名',
    component: 'Input',
    colProps: { span: 6 },
  },
  {
    field: 'mobilePhone',
    label: '手机',
    component: 'Input',
    colProps: { span: 6 },
  },
  {
    field: 'email',
    label: '电子邮件',
    component: 'Input',
    colProps: { span: 6 },
  },
  {
    field: 'organizationIds',
    label: '组织机构',
    component: 'TreeSelect',
    slot: 'organizationTree',
    colProps: { span: 6 },
  },
  {
    field: 'positionIds',
    label: '职位',
    component: 'Select',
    slot: 'positionIdSelect',
    colProps: { span: 6 },
  },
  {
    field: 'roleIds',
    label: '角色',
    component: 'Select',
    slot: 'roleIdSelect',
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
  {
    field: 'isLockout',
    label: '是否锁定',
    component: 'Select',
    componentProps: {
      options: [
        { label: '是', value: true },
        { label: '否', value: false },
      ],
      style: 'width:100%',
    },
    colProps: { span: 6 },
  },
  {
    field: 'isLeader',
    label: '是否部门负责人',
    component: 'Select',
    componentProps: {
      options: [
        { label: '是', value: true },
        { label: '否', value: false },
      ],
      style: 'width:100%',
    },
    colProps: { span: 6 },
  },
];

const checkAccountRule = async (value: string, id: Nullable<number>, accountType: AccountType) => {
  if (value) {
    const exist = await checkAccount({
      id: id,
      account: value,
      accountType: accountType,
    });
    if (exist) {
      let accountTypeText = '';
      switch (accountType) {
        case AccountType.Email:
          accountTypeText = '电子邮件';
          break;
        case AccountType.MobilePhone:
          accountTypeText = '手机';
          break;
        case AccountType.UserName:
          accountTypeText = '用户名';
          break;
        case AccountType.JobNumber:
          accountTypeText = '工号';
          break;
      }
      return Promise.reject(`已经存在${value}的${accountTypeText}`);
    }
  }
  return Promise.resolve();
};

export const getUserNameRules = (id: Nullable<number>): Rule[] => {
  return [
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
    {
      type: 'string',
      validateTrigger: ['change', 'blur'],
      validator: (rules, value) => {
        return checkAccountRule(value, id, AccountType.UserName);
      },
    },
  ];
};

export const getEmailRules = (id: Nullable<number>): Rule[] => {
  return [
    {
      required: true,
      message: '电子邮件格式不允许为空',
    },
    {
      type: 'email',
      message: '电子邮件格式不正确',
      validateTrigger: ['change', 'blur'],
    },
    {
      type: 'string',
      validateTrigger: ['change', 'blur'],
      validator: (rules, value) => {
        return checkAccountRule(value, id, AccountType.Email);
      },
    },
  ];
};

export const getMobilePhoneRules = (id: Nullable<number>): Rule[] => {
  return [
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
    {
      type: 'string',
      validateTrigger: ['change', 'blur'],
      validator: (rules, value) => {
        return checkAccountRule(value, id, AccountType.MobilePhone);
      },
    },
  ];
};

export const getJobNumberRules = (id: Nullable<number>): Rule[] => {
  return [
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
    {
      type: 'string',
      validateTrigger: ['change', 'blur'],
      validator: (rules, value) => {
        return checkAccountRule(value, id, AccountType.JobNumber);
      },
    },
  ];
};

export const userSchemas: FormSchema[] = [
  {
    field: 'userName',
    component: 'Input',
    label: '用户名',
    required: true,
    colProps: {
      span: 12,
    },
  },
  {
    field: 'realName',
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
    field: 'password',
    component: 'InputPassword',
    label: '密码',
    colProps: {
      span: 12,
    },
    helpMessage: '至少8位，包含大小写、数字和特殊符号',
    show: false,
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
    field: 'email',
    component: 'Input',
    label: '电子邮件',
    colProps: {
      span: 12,
    },
  },
  {
    field: 'mobilePhone',
    component: 'Input',
    label: '手机',
    colProps: {
      span: 12,
    },
  },
  {
    field: 'telPhone',
    component: 'Input',
    label: '联系电话',
    colProps: {
      span: 12,
    },
    rules: [
      {
        type: 'string',
        pattern: new RegExp('^(0\\d{2}-\\d{8}(-\\d{1,4})?)|(0\\d{3}-\\d{7,8}(-\\d{1,4})?)$'),
        message: '联系电话格式不正确',
        validateTrigger: ['change', 'blur'],
      },
    ],
  },
  {
    field: 'jobNumber',
    component: 'Input',
    label: '工号',
    colProps: {
      span: 12,
    },
  },
  {
    field: 'surname',
    component: 'Input',
    label: '昵称',
    colProps: {
      span: 12,
    },
  },
  {
    field: 'birthDay',
    component: 'DatePicker',
    label: '生日',
    componentProps: {
      format: 'YYYY-MM-DD',
    },
    colProps: {
      span: 12,
    },
  },
  {
    field: 'sex',
    label: '性别',
    component: 'RadioButtonGroup',
    componentProps: {
      options: sexOptions,
    },
    colProps: {
      span: 12,
    },
  },
  {
    field: 'status',
    label: '状态',
    component: 'RadioButtonGroup',
    componentProps: {
      options: statusOptions,
    },
    colProps: {
      span: 12,
    },
    defaultValue: Status.Valid,
  },
  {
    field: 'lockoutEnabled',
    label: '登录失败后锁定',
    component: 'RadioButtonGroup',
    componentProps: {
      options: [
        { label: '是', value: true },
        { label: '否', value: false },
      ],
    },
    colProps: {
      span: 12,
    },
    defaultValue: true,
  },
];

export const userRoleSchemas: FormSchema[] = [
  {
    field: 'roleNames',
    label: '角色',
    component: 'Select',
    slot: 'roleNamesSlot',
  },
];

export const userDetailSchemas: DescItem[] = [
  {
    label: '用户名',
    field: 'userName',
  },
  {
    label: '真实姓名',
    field: 'realName',
  },
  {
    label: '昵称',
    field: 'surname',
  },
  {
    label: '生日',
    field: 'birthDay',
    render: (value) => {
      return formatToDate(value, 'YYYY-MM-DD');
    },
  },
  {
    label: '性别',
    field: 'sex',
    render: (value) => {
      if (`${value}` === `${Sex.Female}`) {
        return '女';
      }
      if (`${value}` === `${Sex.Male}`) {
        return '男';
      }
      return null;
    },
  },
  {
    label: '工号',
    field: 'jobNumber',
  },
  {
    label: '电子邮件',
    field: 'email',
  },
  {
    label: '手机',
    field: 'mobilePhone',
  },
  {
    label: '联系电话',
    field: 'telPhone',
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
    label: '登陆失败后是否被锁定',
    field: 'lockoutEnabled',
    render: (value) => {
      if (value === true) {
        return commonTagRender('blue', '是');
      } else {
        return commonTagRender('red', '否');
      }
    },
  },
  {
    label: '是否被锁定',
    field: 'isLockout',
    render: (value, data) => {
      if (value === true) {
        return commonTagRender(
          'red',
          `是（${formatToDate(data.lockoutEnd, 'YYYY-MM-DD HH:mm:ss')}）`,
        );
      } else {
        return commonTagRender('blue', '否');
      }
    },
  },
  {
    label: '角色',
    field: 'roles',
    span: 2,
    render: (value: any) => {
      const roleTags = [];
      for (const roleInfo of value) {
        const color = roleInfo.status === Status.Valid ? 'blue' : 'red';
        roleTags.push(commonTagRender(color, roleInfo.realName));
      }
      return roleTags;
    },
  },
  {
    label: '创建时间',
    field: 'createdTime',
    render: (value) => {
      return formatToDate(value, 'YYYY-MM-DD HH:MM:ss');
    },
  },
  {
    label: '最后更新时间',
    field: 'createdTime',
    render: (value) => {
      return formatToDate(value, 'YYYY-MM-DD HH:MM:ss');
    },
  },
];

export const userLockSchemas: FormSchema[] = [
  {
    field: 'lockoutSeconds',
    label: '锁定时长(分)',
    component: 'InputNumber',
    componentProps: {
      style: 'width: 100%',
    },
    rules: [
      {
        required: true,
        message: '锁定时长不允许为空',
      },
      {
        type: 'method',
        required: true,
        validator: (rule, value: number) => {
          if (value > 0) {
            return Promise.resolve();
          }
          return Promise.reject();
        },
        validateTrigger: ['change', 'blur'],
        message: '锁定时长不允许小于0',
      },
    ],
  },
];

export const getIsLeaderOptions = (): OptionsItem[] => {
  return [
    {
      label: '是',
      value: true,
    },
    {
      label: '否',
      value: false,
    },
  ];
};
