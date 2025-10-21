import {HttpClient} from '@angular/common/http';
import {Component, OnInit} from '@angular/core';
import {UserDTO} from "./DTOs/UserDTO";
import {AccountService} from "./services/account.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})

export class AppComponent implements OnInit {

  users: any;
  title = 'DatingApp';


  constructor(private http: HttpClient, private accountService: AccountService) {
  }

  ngOnInit(): void {
    this.getUsers();
    this.setCurrentUser();
  }

  setCurrentUser() {
  const userString = localStorage.getItem('user');
  if (!userString) return; 

  const user: UserDTO = JSON.parse(userString);
  this.accountService.setCurrentUser(user);
}
  getUsers() {
    this.http.get('https://localhost:7226/api/user').subscribe(response => {
      this.users = response;
    }, error => {
      console.log(error);
    });
  }

}
