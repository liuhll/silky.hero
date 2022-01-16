<template>
  <div>
    <Row :gutter="[16, 16]">
      <Col :span="8">
        <Card title="组织结构树">
          <Tree
            title="组织机构"
            :treeData="organizationTreeData"
            :replace-fields="treeFields"
            show-icon
            default-expand-all
          >
            <template #icon><CarryOutOutlined /></template>
          <!-- <template #title="{ key: treeKey, title }">{{title}}</template> -->
          </Tree>
        </Card>
      </Col>
      <Col :span="8">
        <div>组织机构用户</div>
      </Col>
    </Row>
  </div>
</template>
<script lang="ts">
  import { CarryOutOutlined } from '@ant-design/icons-vue';
  import { Tree, Card, Row, Col } from 'ant-design-vue';
  import { defineComponent, nextTick, ref, unref, onMounted } from 'vue';
  import { useOrganizationStore } from '/@/store/modules/organization';
  const organizationStore = useOrganizationStore();
  const treeFields = ref({
    title: 'name',
    key: 'id',
  });
  export default defineComponent({
    components: { Tree, Card, Row, Col, CarryOutOutlined },
    setup() {
      const showIcon = ref<boolean>(true);
      const organizationTreeData = ref([]);
      onMounted(async () => {
        organizationTreeData.value = await organizationStore.getOrganizationTree();
      });
      return {
        organizationTreeData,
        treeFields,
        showIcon,
      };
    },
  });
</script>