declare const _default: {
    name: string;
    data(): {};
    components: {
        AllTodoLists: {
            name: string;
            data(): {
                todoLists: any[];
                form: {
                    listTitle: string;
                };
            };
            methods: {
                getTodoLists(): void;
                addTodoList(listTitle: string): void;
                deleteList(list: any): void;
            };
            created: () => void;
            components: {
                TodoListSummary: {
                    name: string;
                    props: string[];
                    data(): any;
                };
            };
        };
    };
};
export default _default;
