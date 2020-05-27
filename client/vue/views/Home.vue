<template>

  <b-container>

    <h1 class="mb-4">My Lists</h1>

    <TodoLists></TodoLists>
    
    <AddTodoListForm></AddTodoListForm>

  </b-container>
      
</template>

<script>

  import TodoLists from '../components/TodoLists';
  import AddTodoListForm from '../components/AddTodoListForm';

  export default {
    name: "Home",
    data() {
      return {};
    },
    components: {
      TodoLists,
      AddTodoListForm,
    }
  };

</script>