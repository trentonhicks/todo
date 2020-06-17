<template>
    
    <b-list-group>
        <Draggable handle=".sub-item-handle">
            <SubItem
                v-for="item in items"
                :key="item.id"
                :name="item.name">
            </SubItem>
        </Draggable>
    </b-list-group>

</template>

<script>

import SubItem from "./SubItem";
import Draggable from 'vuedraggable';

export default {
    props: ['todoListItem'],
    components: {
        Draggable,
        SubItem
    },
    created() {
        this.items = this.$store.getters.getSubItemsByItemId(this.todoListItem.id);
    },
    data() {
        return {
            items: []
        }
    },
}

</script>