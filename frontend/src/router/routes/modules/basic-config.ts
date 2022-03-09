import type { AppRouteModule } from '/@/router/types';

import { LAYOUT } from '/@/router/constant';
import { t } from '/@/hooks/web/useI18n';

const basicConfig: AppRouteModule = {
  path: '/basicconfig',
  name: 'BasicConfig',
  component: LAYOUT,
  redirect: '/basicconfig/dictionary',
  meta: {
    icon: 'icon-park-outline:setting-config',
    title: '基础配置',
    orderNo: 1000,
  },
  children: [
    {
      path: 'dictionary',
      name: 'Dictionary',
      component: () => import('/@/views/basic-config/dictionary/index.vue'),
      meta: {
        title: '字典管理',
      },
    },
  ],
};

export default basicConfig;
