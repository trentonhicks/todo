<template lang="pug">

.all-todo-lists
    todo-list-summary(
        v-for="item in todoLists"
        v-on:delete-list="deleteList"
        :key="item.id"
        :id="item.id"
        :accountId="item.accountId"
        :listTitle="item.listTitle"
        :completed="item.completed"
        )

    b-button(@click="$bvModal.show('modal-add')" id="add-list-btn") Add list

    b-modal(id="modal-add" title="Add new list")
        b-form(v-on:submit.prevent="addTodoList(form.listTitle)" id="add-list-form")
            b-form-group
                b-form-input(
                type="text"
                placeholder="List name"
                v-model="form.listTitle"
                required)
            b-button(type="submit" variant="primary" class="mr-2") Add
            b-button(variant="secondary" @click="$bvModal.hide('modal-add')") Cancel

</template>

<script lang="ts">
    
import axios from 'axios';
import TodoListSummary from '../components/TodoListSummary.vue';

export default {
    name: 'AllTodoLists',
    data() {
        return {
            todoLists: [],
            form: {
                listTitle: ''
            }
        }
    },
    methods: {
        getTodoLists() : void {
            axios({
                method: 'get',
                url: 'http://localhost:5000/api/lists',
            })
            .then((response) => {
                this.todoLists = response.data;
            }).catch((e) => {
                console.log(e);
            });
        },
        addTodoList(listTitle : string) : void {
            this.$bvModal.hide('modal-add');

            let data = JSON.stringify({
                listTitle
            });

            axios({
                url: 'http://localhost:5000/api/lists',
                method: 'POST',
                data,
                headers: {
                    'content-type': 'application/json'
                }
            }).then((response) => {
                if(response.status == 200) {
                    this.todoLists.push(response.data);
                    this.form.listTitle = ''
                }
            });
        },
        deleteList(list) : void {
            this.$bvModal.msgBoxConfirm(`Are you sure you want to delete ${list.listTitle}?`, {
                size: 'sm',
                okVariant: 'danger',
                okTitle: 'Delete',
                cancelTitle: 'Cancel',
                footerClass: 'p-2',
                hideHeaderClose: false,
            })
            .then(choseToDelete => {
                if(choseToDelete) {
                    axios({
                        method: 'DELETE',
                        url: `http://localhost:5000/api/lists/${list.id}`
                    }).then((response) => {
                        let index = this.todoLists.findIndex(({id}) => id === list.id);
                        if(index !== -1) {
                            this.todoLists.splice(index, 1);
                        }
                    });
                }
            })
        }
    },
    created: function() {
        this.getTodoLists();
    },
    components: {
        TodoListSummary
    }
}

</script>

<style lang="scss">

#modal-add {
    .modal-footer {
        display: none;
    }
}

</style>