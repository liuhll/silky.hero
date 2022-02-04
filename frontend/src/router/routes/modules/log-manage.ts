import type { AppRouteModule } from '/@/router/types';

import { LAYOUT } from '/@/router/constant';
import { t } from '/@/hooks/web/useI18n';

const log: AppRouteModule = {
  path: '/log',
  name: 'Log',
  component: LAYOUT,
  redirect: '/log/audit',
  meta: {
    icon: 'ant-design:audit-outlined',
    title: '日志管理',
    orderNo: 1000,
  },
  children: [
    {
      path: 'audit',
      name: 'Audit',
      component: () => import('/@/views/log-manage/audit/index.vue'),
      meta: {
        title: '审计日志',
      },
    },
  ],
};

export default log;
