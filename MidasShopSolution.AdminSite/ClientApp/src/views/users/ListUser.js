import React from "react";
import axios from "axios";

export class ListUser extends React.Component {
    componentDidMount() {
        axios.get("https://localhost:5001/api/Products/1")
            .then(res => {
                console.log('>>>>> check res: ', res.data);
            })
    }
    render() {
        return (
            <div>Hellow World from List Users</div>
        )
    }
}
