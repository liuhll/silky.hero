<template>
  <BasicDrawer
    v-bind="$attrs"
    @register="registerDrawer"
    showFooter
    title="审计日志详情"
    width="50%"
  >
    <div>审计日志详情</div>
  </BasicDrawer>
</template>

<script lang="ts">
  import { defineComponent } from 'vue';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  export default defineComponent({
    name: 'AuditLogDrawer',
    components: { BasicDrawer },
    setup() {
      const [registerDrawer, { setDrawerProps, closeDrawer }] = useDrawerInner(async (data) => {
        setDrawerProps({ confirmLoading: false });
      });
      return {
        registerDrawer,
      };
    },
  });
</script>
