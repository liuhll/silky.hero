import type { AppRouteModule } from '/@/router/types';

import { LAYOUT } from '/@/router/constant';
import { t } from '/@/hooks/web/useI18n';

const saas: AppRouteModule = {
  path: '/saas',
  name: 'Saas',
  component: LAYOUT,
  redirect: '/saas/tenant',
  meta: {
    icon: 'ant-design:team-outlined',
    title: 'Saas',
    orderNo: 1000,
  },
  children: [
    {
      path: 'tenant',
      name: 'Tenant',
      component: () => import('/@/views/saas/tenant/index.vue'),
      meta: {
        title: '租户管理',
      },
    },
  ],
};

export default saas;
