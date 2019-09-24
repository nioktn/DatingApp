import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpRequest, HttpHeaders } from '@angular/common/http';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  registerMode = false;
  values: any;
  triggered = false;

  constructor(private http: HttpClient) { }

  ngOnInit() {
         }

         

  getValues() {

    let header = {
      headers: new HttpHeaders()
        .set('Authorization',  `Bearer ${localStorage.getItem('token')}`)
    };

    this.http.get('http://localhost:5000/api/notes/5', header).subscribe(response => {
      this.values = response;
    }, error => {
      console.log(error);
    });
    this.triggered = true;
  }



  loggedIn() {
    const token = localStorage.getItem('token');
    // if (token !== '' && token !== null) {
    //   // let myHeaders = new Headers({ 'Authorization': 'Bearer ' + token });
    //   // this.getValues(myHeaders);
    // }
    return !!token;
  }

  registerToggle() {
    this.registerMode = true;
  }

  cancelRegisterMode(registerMode: boolean) {
    this.registerMode = registerMode;
  }

}