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
    mounted() {
      this.$store.state.connection.on("SubItemTrashed", (subItem) => this.refreshLayout(subItem));
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
        refreshLayout(subItem) {
            console.log('Refresh sub-item layout');
        },
        setSubItems() {
            return this.$store.getters.getSubItemsByItemId(this.todoListItem.id);
        }
    },
}

</script>