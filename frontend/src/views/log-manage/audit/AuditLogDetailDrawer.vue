<template>
  <PageWrapper title="详情" contentBackground>
    <template #footer>
      <a-tabs default-active-key="1">
        <a-tab-pane key="1" tab="总体" />
        <a-tab-pane key="2" tab="操作" />
      </a-tabs>
    </template>
  </PageWrapper>
</template>

<script lang="ts">
  import { defineComponent } from 'vue';
  import { PageWrapper } from '/@/components/Page';
  import { Divider, Card, Empty, Descriptions, Steps, Tabs } from 'ant-design-vue';
  export default defineComponent({
    name: 'AuditLogDetailDrawer',
    components: { PageWrapper, Descriptions, Tabs },
  });
</script>
