<template>
    
    <Draggable>
        <SubItem
            v-for="item in items"
            :key="item.id"
            :name="item.name">
        </SubItem>
    </Draggable>

</template>

<script>

import SubItem from "./SubItem";
import Draggable from 'vuedraggable';

export default {
    props: ['itemId'],
    components: {
        Draggable,
        SubItem
    },
    computed: {
        items() {
            return this.$store.subItems.getSubItemsByItemId(this.itemId);
        }
    },
}

</script>