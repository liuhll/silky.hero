import type { AppRouteModule } from '/@/router/types';

import { LAYOUT } from '/@/router/constant';
import { t } from '/@/hooks/web/useI18n';

const authorization: AppRouteModule = {
  path: '/authorization',
  name: 'Authorization',
  component: LAYOUT,
  redirect: '/authorization/user',
  meta: {
    icon: 'ion:key-outline',
    title: '权限管理',
    orderNo: 1000,
  },
  children: [
    {
      path: 'user',
      name: 'User',
      component: () => import('/@/views/authorization/user/index.vue'),
      meta: {
        title: '用户管理',
      },
    },
    {
      path: 'role',
      name: 'Role',
      component: () => import('/@/views/authorization/role/index.vue'),
      meta: {
        title: '角色管理',
      },
    },
    {
      path: 'organization',
      name: 'Organization',
      component: () => import('/@/views/authorization/organization/index.vue'),
      meta: {
        title: '组织机构',
      },
    },
  ],
};

export default authorization;
