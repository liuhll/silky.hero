<template>
  <BasicDrawer
    v-bind="$attrs"
    @register="registerDrawer"
    showFooter
    :title="getTitle"
    width="30%"
    @ok="handleSubmit"
  >
  </BasicDrawer>
</template>

<script lang="ts">
  import { defineComponent, ref, computed, unref } from 'vue';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';

  export default defineComponent({
    name: 'RoleMenuDrawer',
    components: { BasicDrawer },
    setup(_, { emit }) {

      const getTitle = computed(() => '授权角色菜单');

      const [registerDrawer, { setDrawerProps, closeDrawer }] = useDrawerInner(async (data) => {
        setDrawerProps({ confirmLoading: false });
      });

      async function handleSubmit() {
        try {
          setDrawerProps({ confirmLoading: true });
          emit('success', {});
          closeDrawer();
        } finally {
          setDrawerProps({ confirmLoading: false });
        }
      }
      return {
        getTitle,
        handleSubmit,
        registerDrawer,
      };
    },
  });
</script>
