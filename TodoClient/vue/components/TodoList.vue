<template>
  <div class="todo-list-wrapper">
    <div class="todo-list">
      <h1
        class="todo-list-title mb-4"
        @click="showTitleEditor"
        v-if="!editingTitle"
      >{{ list.listTitle }}</h1>

      <b-row>
        <b-col md="8" class="mb-3">
          <b-form class="list-title-editor" v-if="editingTitle" @submit.prevent="updateListTitle">
            <b-form-group>
              <b-form-input
                ref="listTitleInput"
                v-model="form.title"
                id="title"
                maxlength="50"
                required
              ></b-form-input>
            </b-form-group>

            <b-button variant="success" type="submit" class="mb-3">Save</b-button>
          </b-form>

          <TodoListItems :listId="todoListId" :todoListItems="items"></TodoListItems>

          <AddTodoListItemForm class="mt-3" :todoListId="todoListId"></AddTodoListItemForm>
        </b-col>

        <b-col md="4">
          <Contributors
            class="mb-3"
            :todoListContributors="list.contributors"
            :accountContributors="contributors"
          ></Contributors>

          <InviteContributorsForm :listId="this.todoListId"></InviteContributorsForm>
        </b-col>
      </b-row>
    </div>
  </div>
</template>

<script>
import Vue from "vue";
import AddTodoListItemForm from "./AddTodoListItemForm";
import TodoListItems from "./TodoListItems";
import Contributors from "./Contributors";
import InviteContributorsForm from "./InviteContributorsForm";
import VueConfetti from "vue-confetti";

Vue.use(VueConfetti);

export default {
  name: "TodoList",
  props: ["todoListId"],
  data() {
    return {
      items: [],
      editingTitle: false,
      form: {
        title: ""
      }
    };
  },
  async created() {
    await this.$store.dispatch("loadItemsByListId", {
      todoListId: this.todoListId
    });
    this.items = this.getItems();
  },
  destroyed() {
    this.$confetti.stop();
  },
  computed: {
    list() {
      return this.$store.getters.getTodoListById(this.todoListId);
    },
    contributors() {
      return this.$store.getters.contributors;
    },
    allItemsCompleted() {
      return (
        this.items.every(item => item.completed === true) &&
        this.items.length > 0
      );
    }
  },
  watch: {
    allItemsCompleted: function() {
      if (this.allItemsCompleted) {
        this.$confetti.start({
          particles: [{ type: "rect" }],
          particlesPerFrame: 0.5,
          dropRate: 8
        });
      } else {
        this.$confetti.stop();
      }
    }
  },
  components: {
    AddTodoListItemForm,
    TodoListItems,
    Contributors,
    InviteContributorsForm
  },
  methods: {
    getItems() {
      return this.$store.getters.getItemsByListId(this.todoListId);
    },
    showTitleEditor() {
      this.editingTitle = true;

      this.$nextTick(() => {
        this.$refs.listTitleInput.focus();
      });

      this.form.title = this.list.listTitle;
    },
    async updateListTitle() {
      this.editingTitle = false;

      await this.$store.dispatch("updateListTitle", {
        listId: this.todoListId,
        listTitle: this.form.title
      });

      this.form.title = "";
    }
  }
};
</script>