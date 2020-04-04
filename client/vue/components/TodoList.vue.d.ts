declare const _default: {
    name: string;
    props: string[];
    data(): {
        todolist: {};
        todos: any[];
    };
    created: () => void;
    methods: {
        getTodoList(id: number): void;
        getTodoListItems(id: number): void;
    };
    components: {
        TodoItem: {
            name: string;
            props: string[];
            data(): {};
            methods: {
                showItemDetails(): void;
            };
        };
    };
};
export default _default;
