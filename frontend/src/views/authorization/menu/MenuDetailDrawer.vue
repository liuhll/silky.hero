<template>
  <BasicDrawer
    v-bind="$attrs"
    @register="registerDrawer"
    :title="getTitle"
    width="50%"
    destroyOnClose
  >
    <Description
      size="middle"
      :column="2"
      :data="menuDetail"
      :schema="menuDetailSchemas"
      class="enter-y"
    />
  </BasicDrawer>
</template>

<script lang="ts">
  import { defineComponent, ref } from 'vue';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  import { DescItem, Description } from '/@/components/Description/index';
  import { getMenuById } from '/@/api/menu';
  import { getMenuDetailSchemas } from './menu.data';

  export default defineComponent({
    name: 'MenuDetailDrawer',
    components: { BasicDrawer, Description },
    setup() {
      const getTitle = ref('菜单');
      const menuDetail = ref();
      const menuDetailSchemas = ref<DescItem[]>([]);
      const [registerDrawer] = useDrawerInner(async (id) => {
        const data = await getMenuById(id);
        menuDetailSchemas.value = getMenuDetailSchemas(data);
        getTitle.value = data.name;
        menuDetail.value = data;
      });
      return {
        getTitle,
        menuDetail,
        menuDetailSchemas,
        registerDrawer,
      };
    },
  });
</script>
