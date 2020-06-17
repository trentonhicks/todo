<template>
    
    <b-list-group>
        <Draggable handle=".sub-item-handle" v-model="layout">
            <SubItem
                v-for="item in items"
                :key="item.id"
                :subItem="item"
                :listId="todoListItem.listId">
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
        this.items = this.setSubItems();
    },
    data() {
        return {
            items: [],
            layout: []
        }
    },
    methods: {
        getLayout() {

        },
        updateLayout() {

        },
        refreshLayout() {
            
        },
        setSubItems() {
            return this.$store.getters.getSubItemsByItemId(this.todoListItem.id);
        }
    },
}

</script>