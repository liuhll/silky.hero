<template>
  <BasicDrawer
    v-bind="$attrs"
    @register="registerDrawer"
    :title="getTitle"
    width="600px"
    destroyOnClose
  >
    <Description
      size="middle"
      :column="2"
      :data="positionDetail"
      :schema="positionDetailSchemas"
      class="enter-y"
    />
  </BasicDrawer>
</template>

<script lang="ts">
  import { defineComponent, ref } from 'vue';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  import { Description } from '/@/components/Description/index';
  import { getPositionById } from '/@/api/position';
  import { positionDetailSchemas } from './position.data';

  export default defineComponent({
    name: 'PositionDetailDrawer',
    components: { BasicDrawer, Description },
    setup() {
      const getTitle = ref('职位');
      const positionDetail = ref();

      const [registerDrawer] = useDrawerInner(async (id) => {
        const data = await getPositionById(id);
        getTitle.value = data.name;
        positionDetail.value = data;
      });
      return {
        getTitle,
        positionDetail,
        positionDetailSchemas,
        registerDrawer,
      };
    },
  });
</script>
