import type { AppRouteModule } from '/@/router/types';

import { LAYOUT } from '/@/router/constant';
import { t } from '/@/hooks/web/useI18n';

const systemManage: AppRouteModule = {
  path: '/systemmanage',
  name: 'SystemManage',
  component: LAYOUT,
  redirect: '/systemmanage/organization',
  meta: {
    icon: 'ion:grid-outline',
    title: '系统管理',
    orderNo: 1000,
  },
  children: [
    {
      path: 'organization',
      name: 'Organization',
      component: () => import('/@/views/system-manage/organization/index.vue'),
      meta: {
        title: '组织机构',
        icon: 'ion:grid-outline',
      },
    },
  ],
};

export default systemManage;
