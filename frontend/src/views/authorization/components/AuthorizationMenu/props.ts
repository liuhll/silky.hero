import { TreeItem } from "/@/components/Tree/src/typing";

export const basicProps = {
  checkable: {
    type: Boolean,
    default: () => false,
  },
  treeItems: {
    type: Array as PropType<TreeItem[]>,
    default: () => [],
  },
  menuIds: {
    type: Array as PropType<Nullable<Number[]>>,
    default: () => null,
  },
};
