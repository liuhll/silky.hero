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
      :data="tenantDetail"
      :schema="tenantDetailSchemas"
      class="enter-y"
    />
  </BasicDrawer>
</template>

<script lang="ts">
  import { defineComponent, ref } from 'vue';
  import { BasicDrawer, useDrawerInner } from '/@/components/Drawer';
  import { Description } from '/@/components/Description/index';
  import { getTenantById } from '/@/api/tenant';
  import { tenantDetailSchemas } from './tenant.data';

  export default defineComponent({
    name: 'TenantDetailDrawer',
    components: { BasicDrawer, Description },
    setup() {
      const getTitle = ref('租户');
      const tenantDetail = ref();

      const [registerDrawer] = useDrawerInner(async (id) => {
        const data = await getTenantById(id);
        getTitle.value = data.name;
        tenantDetail.value = data;
      });
      return {
        getTitle,
        tenantDetail,
        tenantDetailSchemas,
        registerDrawer,
      };
    },
  });
</script>
