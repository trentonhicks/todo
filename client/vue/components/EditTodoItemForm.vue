<template>
    
    <b-modal :id="`modal-${todoListItem.id}`" :title="todoListItem.name">

        <!-- Name -->
        <b-form @submit.prevent="editingName = false">
            <b-form-group label="Name">
                <b-form-input
                    v-model="todoListItem.name"
                    @focus="editingName = true"
                    @blur="editingName = false"></b-form-input>
            </b-form-group>
            <b-button
                type="submit"
                variant="success"
                size="sm"
                v-if="editingName"
                class="mb-2"
                >Save</b-button>
        </b-form>

        <!-- Notes -->
        <b-form @submit.prevent="editingNotes = false">
            <b-form-group label="Notes">
                <b-form-textarea
                    rows="3"
                    v-model="todoListItem.notes"
                    @focus="editingNotes = true"
                    @blur="editingNotes = false"></b-form-textarea>
            </b-form-group>
            <b-button
                type="submit"
                variant="success"
                size="sm"
                v-if="editingNotes"
                >Save</b-button>
        </b-form>

        <!-- Due Date -->
        <b-form @submit.prevent="editingDueDate = false">
            <b-form-group>
                <label for="due-date">Due Date: {{ todoListItem.dueDate | formatDate }}</label>
                <b-form-datepicker
                    id="due-date"
                    v-model="todoListItem.dueDate"
                    @focus="editingDueDate = true"
                    @blur="editingDueDate = false">
                </b-form-datepicker>
            </b-form-group>
        </b-form>

    </b-modal>

</template>

<script>

    import moment from 'moment';

    export default {
        name: 'EditTodoItemForm',
        props: ['todoListItem'],
        data() {
            return {
                editingName: false,
                editingNotes: false,
                editingDueDate: false
            }
        },
        filters: {
            formatDate: function(value) {
                return moment(value).format('dddd, MMMM Do YYYY');
            },
            monthDay: function(value) {
                return moment(value).format('MMMM Do');
            }
        },
    }

</script>

<style lang="scss" scoped>

    

</style>