export const authReducer = (state, action) => {
  switch (action.type) {
    case 'login':
      return {
        ...state,
        isAuthenticated: true,
        token: action.payload
      };

    case 'logout':
      return {
        ...state,
        isAuthenticated: false,
        token: null
      };

    default:
      return state;
  }
};
