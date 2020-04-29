declare const _default: {
    name: string;
    data(): {};
    components: {
        TodoList: {
            name: string;
            props: string[];
            data(): {
                todoList: {};
                todoListLayout: any[];
                todoListItems: any[];
                form: {};
                listIsEmpty: boolean;
                editingTitle: boolean;
                confetti: boolean;
            };
            created: () => void;
            methods: {
                getTodoList(id: number): void;
                updateListTitle(listTitle: string): void;
                addTodoListItem(name: string, notes: string, dueDate: Date): void;
                deleteTodoListItem(item: any): void;
                checkIfListCompleted(): void;
                updateItemPosition(e: any): void;
            };
            components: {
                TodoItem: {
                    name: string;
                    props: string[];
                    data(): any;
                    methods: {
                        toggleCompleted(): void;
                        editTodoItem(): void;
                        getSubItems(): void;
                        addSubItem(): void;
                    };
                    created: () => void;
                    watch: {
                        checkboxToggle: () => void;
                    };
                    computed: {
                        checkboxToggle(): any;
                        hasSubItems(): any;
                    };
                    filters: {
                        formatDate: (value: any) => string;
                        monthDay: (value: any) => string;
                    };
                    components: {
                        SubItem: {
                            name: string;
                            props: string[];
                            data(): any;
                            directives: {
                                focus: {
                                    inserted(el: any): void;
                                };
                            };
                        };
                    };
                    directives: {
                        focus: {
                            inserted(el: any): void;
                        };
                    };
                };
                draggable: any;
            };
            directives: {
                focus: {
                    inserted(el: any): void;
                };
            };
        };
    };
};
export default _default;
