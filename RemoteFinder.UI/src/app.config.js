export default {
    default: {
        GA: {id: ""},
    },
    development: {
        apiEndpoint: "http://localhost:5000",
        appEndpoint: "http://localhost:8080"
        // GA: {id: ""},
    },
    production: {
        apiEndpoint: "https://api.ulib.online",
        appEndpoint: "https://ulib.online"
        // GA: {id: "UA-227376017-1"},
    },
}