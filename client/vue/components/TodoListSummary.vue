<template lang="pug">
    
.todo-list-summary
    b-card(class="mb-3 bg-light")
        b-button(:to="'/lists/'+ item.id").card-title
            | {{item.listTitle}}
        .todo-list-options
            b-badge(:class="{ 'badge-success': item.completed }").mr-3 {{ item.completed ? 'Completed' : 'In progress' }}
            b-button(variant="info" size="sm" :to="'/lists/'+ item.id").mr-2: b-icon-list-ul
            b-button(variant="danger" size="sm"  @click="$emit('delete-list', item)"): b-icon-trash

</template>

<script lang="ts">

export default {
    name: 'TodoListSummary',
    props: ['id', 'accountId', 'listTitle', 'completed'],
    data() {
        return {
            item: {
                id: this.id,
                accountId: this.accountId,
                listTitle: this.listTitle,
                completed: this.completed
            }
        };
    }
};

</script>

<style lang="scss" scoped>

    .card {
    
        .card-body {
            display: flex;
            align-items: flex-start;
            justify-content: space-between;
            flex-direction: column;
            padding: 0px;

            @media screen and (min-width: 768px) {
                flex-direction: row;
                align-items: center;
            }
        }

        .card-title {
            font-size: 20px;
            margin-bottom: 0px;
            width: 100%;
            text-align: left;
            background: transparent;
            color: #212529;
            border: none;
            font-weight: bold;
            padding: 20px;
            padding-bottom: 0px;

            @media screen and (min-width: 768px) {
                max-width: 400px;
                padding-bottom: 20px;
            }

            &:focus, &:hover, &:not(:disabled):not(.disabled):active {
                background-color: transparent;
                border: none;
                color: #212529;
            }
        }

        .todo-list-options {
            display: flex;
            justify-content: flex-end;
            align-items: center;
            padding: 20px;

            .btn {
                height: 36px;
                width: 36px;
                border-radius: 100px;
                display: flex; 
                align-items: center;
                justify-content: center;
            }

        }

    }

</style>